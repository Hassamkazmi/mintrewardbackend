using Microsoft.AspNetCore.Http;
 using SqlExecutionService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MintRewards.Model.CustomModels
{
    public class CustomModels
    {
    }
    public class BaseEntity
    {

        [PrimaryKey]
        public int? Id { get; set; }
        public Boolean? IsActive { get; set; } = true;
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public int? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public int? UpdatedBy { get; set; }
    }
}
namespace SqlExecutionService
{
    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {
        }
    }
}