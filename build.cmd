@echo off

"tools\nuget.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"

:Build
cls


SET TARGET="Default"

IF NOT [%1]==[] (set TARGET="%1")

SET BUILDMODE="Release"
IF NOT [%2]==[] (set BUILDMODE="%2")

:: because we want to run specific steps inline on qed
:: we need to break the dependency chain
:: this ensures we do a build before running any tests

if %TARGET%=="Default" (SET RunBuild=1)
if %TARGET%=="CreatePackages" (SET RunBuild=1)

if NOT "%RunBuild%"=="" (
	"packages\FAKE\tools\Fake.exe" "build.fsx" "target=BuildApp" "buildMode=%BUILDMODE%"
)

"packages\FAKE\tools\Fake.exe" "build.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%"

:Quit
exit /b %errorlevel%
