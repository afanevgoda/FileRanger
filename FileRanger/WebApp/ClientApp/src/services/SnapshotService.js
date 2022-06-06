import config from "../config";

function getSnapshots(hostName, drive) {
    return fetch(`${config.webAppUrl}/snapshot/GetSnapshots?hostName=${hostName}&drive=${drive}`)
        .then(res => res.json());
}

function getFolders(targetPath, snapshotId) {
    return fetch(`${config.webAppUrl}/snapshot/GetFolders?targetPath=${targetPath}&snapshotId=${snapshotId}`)
        .then(res => res.json());
}

function getFiles(targetPath, snapshotId) {
    return fetch(`${config.webAppUrl}/snapshot/GetFiles?targetPath=${targetPath}&snapshotId=${snapshotId}`)
        .then(res => res.json());
}

function deleteSnapshot(snapshotId) {
    return fetch(`${config.webAppUrl}/snapshot/DeleteSnapshot?snapshotId=${snapshotId}`,
        { method: 'DELETE' });
}


export { getSnapshots, getFolders, getFiles, deleteSnapshot }