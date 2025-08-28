$working = $PSScriptRoot;

Get-ChildItem $working\docs | Remove-Item -Recurse -Force

xmldoc2md $working/Albatross.Reflection/bin/Debug/netstandard2.1/Albatross.Reflection.dll `
	-o $working/docs/ `
	--github-pages `
	--structure tree `
	--back-button
