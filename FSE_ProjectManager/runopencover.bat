@ECHO OFF
REM @echo suppresses command line. 
REM ^ is line continuation character

REM for .NET core projects Nuget packages are installed in %UserProfile%\.nuget\packages
REM The %UserProfile% variable is a special system-wide environment variable 
REM It contains %SystemDrive%\Users\{username}

SET PackagesFolder=%UserProfile%\.nuget\packages
SET OpenCoverVersion=4.7.922
SET ReportGeneratorVersion=4.2.15

REM Some of the APIs used by OpenCover to profile the app are missing in .net Core.     -oldstyle switch fixes this issue
REM "-filter" to selectively include or exclude assemblies and classes from coverage results.
REM DTOs, auto generated designer files, diagrams files, unit test classes are excluded from coverage report
REM Default filters are: -[mscorlib]* -[mscorlib.*]* -[System]* -[System.*]* -[Microsoft.VisualBasic]* 
@ECHO ON
%PackagesFolder%\opencover\%OpenCoverVersion%\tools\OpenCover.Console.exe ^
    -target:"C:/Program Files/dotnet/dotnet.exe" ^
    -targetargs:"test \"PM.Web.Tests1\PM.Web.Tests.csproj\" --configuration Debug --no-build" ^
    -filter:"+[*]*-[*.framework*]* -[NUnit3.*]* -[AutoMapper*]* -[PM.Utilities*]* -[*.DAL*]* -[*.Models*]* -[*.Tests*]* -[*]*.*Config" ^
    -filter:-excludebyfile:*\*Designer.cs -mergebyhash ^
	-skipautoprops ^
	-oldStyle ^
    -register:user ^
    -output:"OpenCoverReport.xml"
@ECHO OFF

REM delete old coverage files
REM /F /Q switches to delete files and directories even with readonly attribute without confirmation
DEL /F /Q .\coverage\*.*

REM Generate HTML based coverage reports
@ECHO ON
REM %PackagesFolder%\ReportGenerator\%ReportGeneratorVersion%\tools\reportgenerator.exe ^ -reports:OpenCoverReport.xml -targetdir:coverage Verbosity: Error

 dotnet %UserProfile%\.nuget\packages\reportgenerator\%ReportGeneratorVersion%\tools\netcoreapp2.1\ReportGenerator.dll "-reports:OpenCoverReport.xml" "-targetdir:coverage" Verbosity: Error
 REM Eg: dotnet C:\Users\krishnamohan\.nuget\packages\reportgenerator\4.1.10\tools\netcoreapp2.1\ReportGenerator.dll "-reports:d:\MyTestOutput.coveragexml" "-targetdir:d:\coveragereport"
REM invoke the html coverage summary in the browser
START "" ".\coverage\index.htm"