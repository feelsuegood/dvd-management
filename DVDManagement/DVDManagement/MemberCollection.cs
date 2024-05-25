using DVDManagement.Models;

namespace DVDManagement.Collections
{
    public class MemberCollection
    {
        private const int MaxMembers = 1000;
        private Member?[] members;  // Member? 로 변경
        private int memberCount;

        public MemberCollection()
        {
            members = new Member?[MaxMembers];  // Member? 로 변경
            memberCount = 0;
        }

        public int MembersCount => memberCount;  // MembersCount 속성 추가

        public void AddMember(Member member)
        {
            if (memberCount < MaxMembers && !IsMemberExist(member))
            {
                members[memberCount] = member;
                memberCount++;
            }
            else
            {
                throw new InvalidOperationException("Cannot add more members or member already exists.");
            }
        }

        public void RemoveMember(Member member)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i]?.FirstName == member.FirstName && members[i]?.LastName == member.LastName)  // null 조건부 연산자 사용
                {
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

        public Member? FindMember(string firstName, string lastName)  // Member? 로 변경
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i]?.FirstName == firstName && members[i]?.LastName == lastName)  // null 조건부 연산자 사용
                {
                    return members[i];
                }
            }
            return null;  // null 반환
        }

        private bool IsMemberExist(Member member)
        {
            for (int i = 0; i < memberCount; i++)
            {
                if (members[i]?.FirstName == member.FirstName && members[i]?.LastName == member.LastName)  // null 조건부 연산자 사용
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
