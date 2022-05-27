namespace Helpers.Extensions;

public static class DateTimeExtensions{
    public static DateTime ToUtcKind(this DateTime dateTime) {
        if (dateTime.Kind == DateTimeKind.Utc)
            return dateTime;
        var dateTimeWithUtcKind = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        return dateTimeWithUtcKind;
    }
}