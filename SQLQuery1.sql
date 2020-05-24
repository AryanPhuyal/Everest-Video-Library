Select a.Id, ar.Id, ar.Id, ar.FirstName,  ar.LastName, 
                     a.Length,a.NoOfCopies, a.NoOfStock, a.CoverImagePath,a.Price, a.ProducerId,
                    a.ReleaseDate from Albums a
                join ArtistAlbums aa on a.Id = aa.AlbumId
                 join Artists ar on aa.ArtistId = ar.Id