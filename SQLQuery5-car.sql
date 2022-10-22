     SELECT c.Id, c.Make, c.Model, c.Color, c.ManufactureDate,
            up.FirstName, up.LastName, up.FirebaseUserId
                   FROM Car c
                   JOIN UserProfile up ON up.id = c.OwnerId