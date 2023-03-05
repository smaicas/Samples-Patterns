$url = $args[0]
if ($null -eq $url) {throw "Repository URL is required"}

git init
git add .
git commit -m "init"
git branch -M master
git remote add origin $url
git push -u origin master