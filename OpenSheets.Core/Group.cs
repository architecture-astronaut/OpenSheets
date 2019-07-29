using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public HashSet<GroupFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public HashSet<GroupPermission> Permissions { get; set; }
        public HashSet<GroupRole> Roles { get; set; }
        public Guid DefaultMemberRole { get; set; }
        public HashSet<Guid> Members { get; set; }
        public GroupKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class GroupRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HashSet<Guid> Members { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class GroupPermission
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Dictionary<GroupPermissionAction, bool> Action { get; set; }
        public GroupPermissionKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public enum GroupPermissionAction
    {
        Invite,
        Kick,
        Edit,
        Archive,
        Broadcast,
        CreateDiscussion,
        CreatePost
    }

    public enum GroupPermissionKind
    {
        Identity,
        Role
    }
}