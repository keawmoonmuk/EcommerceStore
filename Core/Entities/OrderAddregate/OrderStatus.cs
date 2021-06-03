using System.Runtime.Serialization;

namespace Core.Entities.OrderAddregate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Panding")]
        Pending,

        [EnumMember(Value ="Payment Received")]
        PaymentRecevied,

        [EnumMember(Value ="Payment Failed")]
        PaymentFailed

    }
}