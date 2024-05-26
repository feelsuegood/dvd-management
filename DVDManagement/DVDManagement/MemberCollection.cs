using System;

namespace DVDManagement
{
    public class MemberCollection
    {
        private const int MaxMembers = 1000;
        private Member[] members;
        private int memberCount;

        public MemberCollection()
        {
            members = new Member[MaxMembers];
            memberCount = 0;
        }

        public int MembersCount => memberCount;

        public void AddMember(Member member)
        {
            if (memberCount < MaxMembers && !IsMemberExist(member.FirstName, member.LastName))
            {
                members[memberCount] = member;
                memberCount++;
            }
            else
            {
                throw new InvalidOperationException("Cannot add more members or member already exists.");
            }
        }

        public void RemoveMember(string firstName, string lastName)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    if (members[i].BorrowedMovies.Length > 0)
                    {
                        throw new InvalidOperationException("Member must return all DVDs before being removed.");
                    }

                    for (int j = i; j < memberCount - 1; j++)
                    {
                        members[j] = members[j + 1];
                    }
                    members[memberCount - 1] = null;
                    memberCount--;
                    break;
                }
            }
        }

        public string FindMemberPhoneNumber(string firstName, string lastName)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return members[i].PhoneNumber;
                }
            }
            return "Member not found.";
        }

        public Member[] FindMembersByMovie(string title)
        {
            Member[] rentingMembers = new Member[memberCount];
            int rentingMemberCount = 0;

            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].HasBorrowedMovie(title))
                {
                    rentingMembers[rentingMemberCount] = members[i];
                    rentingMemberCount++;
                }
            }

            Array.Resize(ref rentingMembers, rentingMemberCount);
            return rentingMembers;
        }

        public Member? FindMember(string firstName, string lastName)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return members[i];
                }
            }
            return null;
        }

        public bool IsMemberExist(string firstName, string lastName)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return true;
                }
            }
            return false;
        }

        public Member? GetMember(int index)
        {
            if (index >= 0 && index < memberCount)
            {
                return members[index];
            }
            return null;
        }
    }
}
