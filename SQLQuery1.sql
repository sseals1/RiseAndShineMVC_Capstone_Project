SELECT FirebaseUserId, Id, [FirstName], LastName, Email, Phone, Address, UserTypeId
                        FROM UserProfile
                        WHERE FirebaseUserId = @firebaseUserId