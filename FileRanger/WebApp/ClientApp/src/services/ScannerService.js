import config from "../config";

function getScanners() {
    return fetch(`${config.webAppUrl}/scanner/GetAvailableScanners`)
        .then(res => res.json());
}

export { getScanners }