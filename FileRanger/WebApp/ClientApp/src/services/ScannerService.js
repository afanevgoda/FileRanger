
function getScanners() {
    return fetch(`http://localhost:5000/scanner/GetAvailableScanners`)
        .then(res => res.json());
}

export { getScanners }