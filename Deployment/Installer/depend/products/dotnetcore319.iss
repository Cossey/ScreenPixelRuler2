[CustomMessages]
dotnetcore312d_title=.NET Core 3.1.9 Desktop 32-Bit Runtime
dotnetcore312d_title_x64=.NET Core 3.1.9 Desktop 64-Bit Runtime
dotnetcore312d_size=46.3 MB
dotnetcore312d_size_x64=51.8 MB

[Code]
const
	dotnetcore319d_url = 'http://download.visualstudio.microsoft.com/download/pr/1419cd1f-64ac-44c7-bfe0-937fd5e5f39a/e72ec98df78dfbb3a5bbf35b66cb7c15/windowsdesktop-runtime-3.1.9-win-x86.exe';
	dotnetcore319d_url_x64 = 'https://download.visualstudio.microsoft.com/download/pr/6a3a8a8b-4aaa-4d3f-bce6-b512f2ef5a2c/e6963fbe57cdd8258a9f0997cc3a6669/windowsdesktop-runtime-3.1.9-win-x64.exe';


function NetCore319Desktop_32NeedsInstall: Boolean;
var
  ResultCode: Integer;
begin
  Result := false;
  if ExpandConstant('{param:skipfw|false}') = 'false' then
  begin
    if not IsWin64 then
    begin
      Exec('cmd.exe', '/c dotnet --list-runtimes | find /n "Microsoft.WindowsDesktop.App 3.1"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      if ResultCode = 1 then 
      begin 
        Result := true;
      end;
    end;
  end;
end;

function NetCore319Desktop_64NeedsInstall: Boolean;
var
  ResultCode: Integer;
begin
  Result := false;
  if ExpandConstant('{param:skipfw|false}') = 'false' then
  begin
    if IsWin64 then
    begin
      Exec('cmd.exe', '/c dotnet --list-runtimes | find /n "Microsoft.WindowsDesktop.App 3.1"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      if ResultCode = 1 then 
      begin
        Result := true;
      end;
    end;  
  end;
end;

function NetCore319Desktop_NeedsInstall: Boolean;
begin
  if IsWin64 then 
  begin
    Result := NetCore319Desktop_64NeedsInstall();
  end
  else 
  begin
    Result := NetCore319Desktop_32NeedsInstall();
  end;
end;

procedure dotnetcore319desktop();
begin
	if (NetCore319Desktop_NeedsInstall()) then
		AddProduct('windowsdesktop-runtime-3.1.9-win' + GetArchitectureString(true) + '.exe',
			'/install /quiet /norestart',
			GetString(CustomMessage('dotnetcore319d_title'), CustomMessage('dotnetcore319d_title_x64'), true),
			GetString(CustomMessage('dotnetcore319d_size'), CustomMessage('dotnetcore319d_size_x64'), true),
			GetString(dotnetcore319d_url, dotnetcore319d_url_x64, true),
			false, false, false);
end;


[Setup]
