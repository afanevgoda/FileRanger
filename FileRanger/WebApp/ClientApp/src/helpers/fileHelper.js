function isDrivePath(path) {
    if (path === undefined)
        return false;
    let foldersInPath = path.split("\\");
    foldersInPath = foldersInPath.filter(x => x !== "");
    return foldersInPath.length === 1;
}

export { isDrivePath } 