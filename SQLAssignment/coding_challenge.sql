create database Virtual_ArtGallery

use Virtual_ArtGallery;

-- Create the Artists table
CREATE TABLE Artists (
 ArtistID INT PRIMARY KEY,
 Name VARCHAR(255) NOT NULL,
 Biography TEXT,
 Nationality VARCHAR(100));

-- Create the Categories table
CREATE TABLE Categories (
 CategoryID INT PRIMARY KEY,
 Name VARCHAR(100) NOT NULL);

-- Create the Artworks table
CREATE TABLE Artworks (
 ArtworkID INT PRIMARY KEY,
 Title VARCHAR(255) NOT NULL,
 ArtistID INT,
 CategoryID INT,
 Year INT,
 Description TEXT,
 ImageURL VARCHAR(255),
 FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID),
 FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID));

-- Create the Exhibitions table
 CREATE TABLE Exhibitions (
 ExhibitionID INT PRIMARY KEY,
 Title VARCHAR(255) NOT NULL,
 StartDate DATE,
 EndDate DATE,
 Description TEXT);

-- Create a table to associate artworks with exhibitions
CREATE TABLE ExhibitionArtworks (
 ExhibitionID int,
 ArtworkID INT,
 PRIMARY KEY (ExhibitionID, ArtworkID),
 FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID),
 FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID));

 -- Insert sample data into the Artists table
INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'),
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'),
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian'),
(4, 'Alan', 'Spanish painter and sculptor.', 'Spanish'),
(5, 'Vindiesel', 'Dutch post-impression', 'Dutch'),
(6, 'Keanu Reeves', 'Italian polymath ', 'American');

INSERT INTO Categories (CategoryID, Name) VALUES
(1, 'Painting'),
(2, 'Sculpture'),
(3, 'Photography');

-- Insert sample data into the Artworks table
INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'),
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'),
(3, 'Guernica', 1, 1, 1937, 'Pablo Picasso powerful anti-war mural.', 'guernica.jpg'),
(4, 'Night Starry', 4, 1, 1899, 'A famous painting', 'night_starry.jpg'),
(5, 'Lisa-Mona', 5, 1, 1603, 'The iconic portrait.', 'lisa_mona.jpg'),
(6, 'John Wick', 6, 1, 1947, 'Powerful anti-war mural.', 'John_wick.jpg');
;

-- Insert sample data into the Exhibitions table
INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'),
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.');

-- Insert artworks into exhibitions
INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 2),
(1, 5),
(2, 4);



--1. Retrieve the names of all artists along with the number of artworks they have in the gallery, and list them in descending order of the number of artworks.
select Name, COUNT(aw.ArtworkID)as noofArtworks from Artists a 
JOIN Artworks aw ON a.ArtistID = aw.ArtistID 
group by Name 
order by noofArtworks DESC;

--List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order them by the year in ascending order.
select Name,aw.Title,aw.YEAR from Artists a
JOIN Artworks aw ON a.ArtistID=aw.ArtistID 
where a.Nationality IN ('Dutch', 'Spanish') 
order by aw.Year ASC;

--Find the names of all artists who have artworks in the 'Painting' category, and the number of artworks they have in this category.
select a.Name, COUNT(aw.ArtworkID) from Artists a 
JOIN Artworks aw ON a.ArtistID=aw.ArtistID 
JOIN Categories c ON aw.CategoryID =c.CategoryID 
where c.Name ='Painting' 
group by a.Name

--4. List the names of artworks from the 'Modern Art Masterpieces' exhibition, along with their artists and categories.
SELECT a.Name,aw.Title,c.name from Artworks aw
JOIN ExhibitionArtworks es ON aw.ArtworkID = es.ArtworkID 
JOIN Exhibitions e ON es.ExhibitionID = e.ExhibitionID 
JOIN Artists a ON aw.ArtistID = a.ArtistID 
JOIN Categories c ON aw.CategoryID = c.CategoryID
where e.Title = 'Modern Art Masterpieces'


--5. Find the artists who have more than two artworks in the gallery.
select Name from Artists a
JOIN Artworks aw ON a.ArtistID = aw.ArtistID 
group by a.Name HAVING COUNT(aw.ArtworkID) > 2

--6. Find the titles of artworks that were exhibited in both 'Modern Art Masterpieces' and 'Renaissance Art' exhibitions
select a.Title FROM Artworks a
JOIN ExhibitionArtworks es ON a.ArtworkID =es.ArtworkID 
JOIN Exhibitions e ON es.ExhibitionID =e.ExhibitionID 
where e.Title IN ('Modern Art Masterpieces', 'Renaissance Art') 
group by a.Title HAVING COUNT(DISTINCT e.Title) =2

--7. Find the total number of artworks in each category
select c.Name, COUNT(a.ArtworkID) from Categories c 
LEFT JOIN Artworks a ON c.CategoryID =a.CategoryID 
group by c.Name
--select c.Name, COUNT(a.ArtworkID) from Categories c JOIN Artworks a ON c.CategoryID =a.CategoryID group by c.Name

--8. List artists who have more than 3 artworks in the gallery.
select Name from Artists a 
JOIN Artworks aw ON a.ArtistID = aw.ArtistID 
group by a.Name HAVING COUNT(aw.ArtworkID) > 3


--9. Find the artworks created by artists from a specific nationality (e.g., Spanish).
select a.Name,aw.Title from Artists a 
JOIN Artworks aw ON a.ArtistID=aw.ArtistID 
where a.Nationality ='Dutch'

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.


--11. Find all the artworks that have not been included in any exhibition.
select ArtistID,Title from Artworks a
LEFT JOIN ExhibitionArtworks e ON a.ArtworkID =e.ArtworkID 
where e.ArtworkID IS NULL

--12. List artists who have created artworks in all available categories.
select Name from Artists a
JOIN Artworks aw ON a.ArtistID =aw.ArtistID 
where aw.CategoryID =ALL
(select CategoryID from Categories)


--13. List the total number of artworks in each category.
select Name, COUNT(a.ArtworkID)from Categories c
LEFT JOIN Artworks a ON c.CategoryID = a.CategoryID 
group by c.Name

--14. Find the artists who have more than 2 artworks in the gallery.
select a.ArtistID, a.Name from Artists a
JOIN Artworks aw ON a.ArtistID =aw.ArtistID
where a.ArtistID IN (
select ArtistID from Artworks
group by ArtistID
HAVING COUNT(ArtworkID) > 2
);

--15. List the categories with the average year of artworks they contain, only for categories with more than 1 artwork.
--16. Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition.
select a.ArtworkID,a.Title,a.ArtistID from Artworks a
JOIN ExhibitionArtworks es ON a.ArtworkID =es.ArtworkID 
JOIN Exhibitions e ON es.ExhibitionID =e.ExhibitionID 
where e.Title ='Modern Art Masterpieces'

--17. Find the categories where the average year of artworks is greater than the average year of all artworks.
select Name from Categories c
JOIN Artworks a ON c.CategoryID =a.CategoryID
group by c.Name HAVING AVG(a.Year) >(select AVG(Year) from Artworks)

--18. List the artworks that were not exhibited in any exhibition.
select * from Artworks a
LEFT JOIN ExhibitionArtworks e ON a.ArtworkID = e.ArtworkID 
where e.ArtworkID IS NULL


--19. Show artists who have artworks in the same category as "Mona Lisa."
select DISTINCT Name from Artists a
JOIN Artworks aw ON a.ArtistID = aw.ArtistID
where aw.CategoryID = (
select CategoryID from Artworks
where Title = 'Mona Lisa'
);

--20. List the names of artists and the number of artworks they have in the gallery.
select Name , COUNT(ArtworkID) from Artists a 
JOIN Artworks aw ON a.ArtistID =aw.ArtistID 
group by Name

