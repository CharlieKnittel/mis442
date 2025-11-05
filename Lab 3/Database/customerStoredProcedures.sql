DELIMITER // 
CREATE PROCEDURE usp_CustomerDelete (in custId int, in conCurrId int)
BEGIN
	Delete from customers where customerid = custId and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_CustomerCreate (out custId int, in name_p varchar(100), in address_p varchar(50), in city_p varchar(20), 
	in state_p char(2), in zipCode_p char(15))
BEGIN
	Insert into customers (name, address, city, state, zipcode) values (name_p, address_p, city_p, state_p, zipCode_p);
    SELECT LAST_INSERT_ID() into custId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerSelect (in custId int)
BEGIN
	Select * from customers where customerid=custId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerSelectAll ()
BEGIN
	Select * from customers order by name;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerUpdate (in custId int, in name_p varchar(100), in address_p varchar(50), in city_p varchar(20), 
	in state_p char(2), in zipCode_p char(15), in conCurrId int)
BEGIN
	Update customers
    Set name = name_p, address = address_p, city = city_p, state = state_p, zipcode = zipCode_p, concurrencyid = (concurrencyid + 1)
    Where customerid = custId and concurrencyid = conCurrId;
END //
DELIMITER ;