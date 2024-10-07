using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Enum
{
    public enum AdoptionStatus
    {
        Pending = 0, // chưa giải quyết
        Accepted = 1, //Chấp nhận
        Rejected = 2, // Từ chối
        Cancel = 3, // Hủy
        Preparing = 4,  // Đang chuẩn bị
        Delivered = 5, // Giao cho tài xế
        Finish = 6, // Hoàn tất
        Confirm = 7, //User đã xác nhận
    }
}
