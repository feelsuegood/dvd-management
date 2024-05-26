namespace DVDManagement
{
    public class MemberCollection
    {
        private const int MaxMembers = 1000;
        private Member[] members;
        private int memberCount;
        private const string filePath = "members.txt";

        public MemberCollection()
        {
            members = new Member[MaxMembers];
            memberCount = 0;
            LoadMembers();
        }

        public int MembersCount => memberCount;

        public void AddMember(Member member)
        {
            if (memberCount < MaxMembers && !IsMemberExist(member.FirstName, member.LastName))
            {
                members[memberCount] = member;
                memberCount++;
                SaveMembers();
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
                    // BorrowedMovies 배열에 실제로 빌린 영화가 있는지 확인
                    if (members[i].BorrowedMovies.Any(m => m != null))
                    {
                        throw new InvalidOperationException("Member must return all DVDs before being removed.");
                    }

                    for (int j = i; j < memberCount - 1; j++)
                    {
                        members[j] = members[j + 1];
                    }
                    members[memberCount - 1] = null;
                    memberCount--;
                    SaveMembers();
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

        public void SaveMembers()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < memberCount; i++)
                {
                    writer.WriteLine(members[i].Serialize());
                }
            }
        }

        public void LoadMembers()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var member = Member.Deserialize(line);
                        members[memberCount++] = member;
                    }
                }
            }
        }
    }
}
