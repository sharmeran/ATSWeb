using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Enums
{
    [DataContract]
    public enum ActionStatusEnum
    {
        /// <summary>
        /// NO Error
        /// </summary>
        [DataMember]
        NoError = 0,

        /// <summary>
        /// Error thrown by an Exceptin
        /// </summary>
        [DataMember]
        Exception = -1,

        /// <summary>
        /// Error if Record is already created 
        /// </summary>
        [DataMember]
        DuplicateID = -2,

        /// <summary>
        /// Cannot delete this Item
        /// </summary>
        [DataMember]
        CannotDelete = -3,

        /// <summary>
        /// Item not found
        /// </summary>
        [DataMember]
        NotFound = -4,

        /// <summary>
        ///  Cannot insert this Item
        /// </summary>
        [DataMember]
        CannotInsert = -5,

        /// <summary>
        ///  Cannot update this Item
        [DataMember]
        CannotUpdate = -6,

        /// <summary>
        /// The value cannot be empty or null
        /// </summary>
        [DataMember]
        CannotBeEmptyOrNull = -7,

        /// <summary>
        /// Cannot check the DataBase value
        /// </summary>
        [DataMember]
        CannotCheck = -8,
        [DataMember]
        FaildLogin = -9,
        [DataMember]
        IsExist = -10
    }
}
