//todo: add moment.js if amount of dates formatting will be big enough

function defaultFormat(date) {
    let result;
    if (typeof date == 'string')
        result = new Date(date);
    return result.toLocaleString();
}

export { defaultFormat }