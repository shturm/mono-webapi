#!/bin/bash

if [[ "$#" -ne 1 ]]; then
	echo "
	Renames the solution, all projects and all occurences of the text \"MonoWebApi\"
	with specified <NewProjectName>.

	Usage is:
		./rename-solution.sh <NewProjectName>
	"
	exit
fi


for file in $(find . -depth -name 'MonoWebApi*')
do
	echo "Renaming $file"
	mv $file $(echo $file | sed -r "s/MonoWebApi/$1/g")
done
echo "------------------------------------"

for file in $(find . -depth -name 'MonoWebApi*')
do
	echo "Renaming $file"
	mv $file $(echo $file | sed -r "s/MonoWebApi/$1/g")
done
echo "------------------------------------"

for file in $(find . -name '*.cs' -o -name '*.csproj' -o -name '*.sln' -o -name '*.config' -name '*.asax')
do
	echo "Replacing in $file"
	sed -i -r "s/MonoWebApi/$1/g" $file
done
