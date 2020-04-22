namespace BO
{
    public enum IdType
    {
        ID,
        PASSPORT
    }
    public enum PersonStatus
    {
        ACTIVE,
        INACTIVE
    }
    public enum RequestStatus
    {
       
        OPEN,
        EXPIRED,
        CANCELLED,
        ORDERED,
        IRRELEVANT
    }
    public enum Areas
    {
        הכל,
        צפון,
        דרום,
        מרכז,
        ירושלים
    }
    public enum HostingType
    {
        צימר,
        מלון,
        קמפינג,
        וילה
    }
    public enum Answer
    {
        כן,
        אולי,
        לא
    }
    public enum OrderStatus
    {
        PROCESSING,
        MAIL_SENT,
        APPROVED,
        NO_CLIENT_RESPONSE,
        IGNORED_CLOSED,
        IRRELEVANT
    }
    public enum ActivityStatus//extra
    {
        ACTIVE,
        INACTIVE
    }
}