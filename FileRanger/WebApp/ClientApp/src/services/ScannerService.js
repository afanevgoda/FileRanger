import config from "../config";

function getScanners() {
    return fetch(`${config.webAppUrl}/scanner/GetAvailableScanners`)
        .then(res => res.json());
}

function startScan(targetDrive) {
    return fetch(`${config.webAppUrl}/scanner/StartScan?targetDisk=${targetDrive}`, { method: 'POST' });
}


export { getScanners, startScan }