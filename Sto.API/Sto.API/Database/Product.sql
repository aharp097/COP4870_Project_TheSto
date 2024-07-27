----CREATE SCHEMA Product
--CREATE PROCEDURE Product.InsertProduct
--@Id int output,
--@Name nvarchar(255),
--@Description nvarchar (max),
--@Stock int,
--@Price numeric(10,2),
--@Bogo bit,
--@MarkedDown bit,
--@MarkDownPercent int

--AS
--BEGIN
--INSERT INTO PRODUCT([Name], [Description], [Price], [Stock], [Bogo], [MarkedDown], [MarkDownPercent])
--VALUES (@Name, @Description, @Price, @Stock, @Bogo, @MarkedDown, @MarkDownPercent)

--SET @Id = SCOPE_IDENTITY()
--END


--CREATE PROCEDURE Product.UpdateProduct
--@Id int,
--@Name nvarchar(255),
--@Description nvarchar (max),
--@Stock int,
--@Price numeric(10,2),
--@Bogo bit,
--@MarkedDown bit,
--@MarkDownPercent int

--AS
--BEGIN
--UPDATE PRODUCT
--SET 
--Name = @Name,
--Description = @Description,
--Price = @Price, 
--Stock = @Stock, 
--Bogo = @Bogo, 
--MarkedDown = @MarkedDown, 
--MarkdownPercent = @MarkDownPercent

--WHERE Id = @Id
--END

--CREATE PROCEDURE Delete @Id int
--AS
--DELETE Product where Id = @Id
