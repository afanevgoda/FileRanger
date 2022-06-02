import config from "../config";

function getData(targetFolder) {
    console.log(targetFolder)
    return fetch(`${config.webAppUrl}/scanner/GetFolders?targetPath=${targetFolder}`)
        .then(res => res.json());
}



export { getData };