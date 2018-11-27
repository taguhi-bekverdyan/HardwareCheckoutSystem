using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWeb.Model
{
    public class Device
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime? MaxPeriod { get; set; }
        public byte[] Image { get; set; }
        public DeviceStatus Status { get; set; }
        public Permission Permission { get; set; }
        public Device()
        {
            Id = Guid.NewGuid();
        }


    }

    public enum DeviceStatus
    {
        Other,
        StatusOne,
        StatusTwo,
        StatusThree,
        StatusFour,
        StatusFive
    }
}
