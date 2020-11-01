#define RootPath "..\..\"
#define AppPath RootPath + "ScreenPixelRuler2\bin\Release\netcoreapp3.1\"

#define ApplicationVersion GetFileVersion(AppPath + 'ScreenPixelRuler.dll')

[Setup]
AppId=91F2F59D-F956-4226-B263-1DCE83C12DE0
AppCopyright=2020 Stewart Cossey
AppName=Screen Pixel Ruler
AppVersion={#ApplicationVersion}
VersionInfoVersion={#ApplicationVersion}
DefaultDirName={code:GetProgramFiles}\ScreenPixelRuler
OutputBaseFilename=ScreenPixelRuler-{#ApplicationVersion}
UsePreviousTasks=yes
LicenseFile={#RootPath}\LICENSE.md

[Files]
Source: "{#AppPath}\*"; DestDir: "{app}"; Excludes: "*.pdb"; Flags: recursesubdirs createallsubdirs
Source: "{#RootPath}\LICENSE.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RootPath}\Resources\*.thm"; DestDir: "{userappdata}\screenpixelruler"; Flags: ignoreversion
Source: "{#RootPath}\README.md"; DestDir: "{app}";  Flags: ignoreversion

[CustomMessages]
#include "depend\lang\english.iss"
#include "depend\products.iss"
#include "depend\products\dotnetcore319.iss"


[Code]
function IsUpgrade: Boolean;
var
    Value: string;
    UninstallKey: string;
begin
    UninstallKey := 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' +
        ExpandConstant('{#SetupSetting("AppId")}') + '_is1';
    Result := (RegQueryStringValue(HKLM, UninstallKey, 'UninstallString', Value) or
        RegQueryStringValue(HKCU, UninstallKey, 'UninstallString', Value)) and (Value <> '');
end;

function GetProgramFiles(Param: string): string;
begin
  if IsWin64 then Result := ExpandConstant('{pf64}')
    else Result := ExpandConstant('{pf32}')
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = wpSelectTasks) and IsUpgrade;
end;

function InitializeSetup(): boolean;
begin
  dotnetcore319desktop();
  Result := true;
end;