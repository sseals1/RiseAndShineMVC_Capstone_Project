      SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.Id AS UserTypeId, ut.Name AS UserTypeName 
                   FROM UserProfile up

                        JOIN UserType ut ON ut.Id = up.UserTypeId
                        --c.Make, c.Model, c.Color, c.ImageUrl, c.ManufactureDate,
                        --sr.Note
                        --, d.DetailPackageName, d.PackagePrice
                        --JOIN Car c ON c.OwnerId = up.Id
                        --JOIN ServiceRequest sr ON sr.CarId = c.Id
                        --JOIN DetailType d ON d.Id = sr.DetailTypeId

                   --WHERE FirebaseUserId = '52EvUHROG9SlfkvSsmOykP5MTid2'