# Pass4Kee
The first time you open the app it asks you to enter your password and confirm with windows hello.
Any other time you open the app it goes directly to check with windows hello and if succesfull puts the password in the clipboard.
If you cancel, you can change the password.

I recomend to use this app combined with AutoHotKey with a script like this:

^!q::
  ClipSaved := ClipboardAll
  Clipboard :=
  Run, C:\Users\{yourusername}\Desktop\Pass4Kee
  WinWait, Pass4Kee,
  
  ClipWait
  Sleep 300
  SendRaw, %Clipboard%
  Send, {Enter}
  Clipboard := ClipSaved
  
 Return



You obiously need to put a Pass4Kee shortcut in the desktop
