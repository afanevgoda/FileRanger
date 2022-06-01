//todo: localhost -> configuration 
function getSnapshots(hostName, drive) {
    return fetch(`http://localhost:5000/snapshot/GetSnapshots?hostName=${hostName}&drive=${drive}`)
        .then(res => res.json());
}

function getFolders(targetPath, snapshotId) {
    return fetch(`http://localhost:5000/snapshot/GetFolders?targetPath=${targetPath}&snapshotId=${snapshotId}`)
        .then(res => res.json());
}

function getFiles(targetPath, snapshotId) {
    return fetch(`http://localhost:5000/snapshot/GetFiles?targetPath=${targetPath}&snapshotId=${snapshotId}`)
        .then(res => res.json());
}

export { getSnapshots, getFolders, getFiles }