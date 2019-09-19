FROM docker-registry.kls.fielmann.net/kls-base/kls-mono-dotnet-cli AS build
WORKDIR /build
COPY . ./
RUN dotnet msbuild -restore -maxcpucount:1 -verbosity:m -nologo -consoleloggerparameters:Summary -target:Build -property:RuntimeIdentifier=linux-x64 -property:TargetFramework=net462 -property:Configuration=Release -property:OutputPath=out ExceptionTestReal.sln
RUN cp -R ExceptionTestReal/out /out-main

FROM docker-registry.kls.fielmann.net/kls-base/kls-mono-kafka-nunit:latest AS integration-test
WORKDIR /test
COPY --from=build /build ./
RUN mono /tools/NUnit.ConsoleRunner/tools/nunit3-console.exe --teamcity --noresult ExceptionTestRealTest/out/ExceptionTestRealTest.dll

FROM docker-registry.kls.fielmann.net/kls-base/kls-mono-kafka:latest AS final
COPY --from=build /out-main /app
WORKDIR /app
ENTRYPOINT [ "mono", "ExceptionTestReal.exe" ]