
function getData(targetFolder) {
    console.log(targetFolder)
    return fetch(`http://localhost:5000/scanner/GetFolders?targetPath=${targetFolder}`)
        .then(res => res.json());
}



export { getData };