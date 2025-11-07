DELIMITER // 
CREATE PROCEDURE usp_ProductDelete (in prodId int, in conCurrId int)
BEGIN
	Delete from products where productid = prodId and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_ProductCreate (out prodId int, in productCode_p char(10), in description_p varchar(50), in unitPrice_p decimal(10,4), 
	in onHandQuantity_p int)
BEGIN
	Insert into products (productcode, description, unitPrice, onHandQuantity) values (productcode_p, description_p, unitPrice_p, 
		onHandQuantity_p);
    SELECT LAST_INSERT_ID() into prodId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductSelect (in prodId int)
BEGIN
	Select * from products where productid=prodId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductSelectAll ()
BEGIN
	Select * from products order by productid;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductUpdate (in prodId int, in productCode_p char(10), in description_p varchar(50), in unitPrice_p decimal(10,4), 
	in onHandQuantity_p int, in conCurrId int)
BEGIN
	Update products
    Set productCode = productCode_p, description = description_p, unitPrice = unitPrice_p, onHandQuantity = onHandQuantity_p, 
		concurrencyid = (concurrencyid + 1)
    Where productid = prodId and concurrencyid = conCurrId;
END //
DELIMITER ;