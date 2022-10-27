            SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.[Name] AS UserTypeName, 
                        c.Make, c.Model, c.Color, c.ImageUrl, c.ManufactureDate,
                        sr.Note,
                        d.DetailPackageName, d.PackagePrice
                   FROM UserProfile up

                        JOIN UserType ut ON ut.Id = up.UserTypeId
                        LEFT JOIN Car c ON c.OwnerId = up.Id
                        LEFT JOIN ServiceRequest sr ON sr.CarId = c.Id
                        LEFT JOIN DetailType d ON d.Id = sr.DetailTypeId

                    WHERE ut.[Name] = 'Owner'