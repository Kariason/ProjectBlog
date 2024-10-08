﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBlog.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string RoleDescription { get; set; } = "";

        [ForeignKey("RoleId")]
        public List<User> Users { get; set; } = new List<User>();
    }
}
