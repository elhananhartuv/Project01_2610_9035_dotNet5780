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
        NEW,//extra
        OPEN,
        EXPIRED,
        CANCELLED,
        ORDERED
    }
    public enum Areas
    {
        ALL,
        NORTH,
        SOUTH,
        CENTER,
        JERUSALEM
    }
    public enum HostingType
    {
        ZIMMER,
        HOTEL,
        CAMPING,
        VILLA
    }
    public enum Answer
    {
        YES,
        MAYBE,
        NO
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