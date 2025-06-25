namespace MyRoad.Domain.Users
{
    public static class UserMapper
    {
        public static void MapUpdatedUser(this User existingUser, User updatedUser)
        {
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
        }
    }
}
