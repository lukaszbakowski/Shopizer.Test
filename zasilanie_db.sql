
DELIMITER //


	-- DECLARE counter INT default 0;
    
    SELECT MAX(CAST(`PRODUCT`.`PRODUCT_ID` as SIGNED))
    FROM SALESMANAGER.PRODUCT into @counter;
    -- ORDER BY `PRODUCT`.`PRODUCT_ID` DESC LIMIT 1;
    SET @counter = @counter + 1;
	-- WHILE counter < 2 DO
		-- SELECT * FROM SALESMANAGER.PRODUCT order by PRODUCT_ID limit 55;
			-- delete from SALESMANAGER.PRODUCT where product_id = 4;
			-- delete from SALESMANAGER.PRODUCT_IMAGE where product_image_id = 4;
			-- delete from SALESMANAGER.PRODUCT_AVAILABILITY where PRODUCT_ID = 4;
			-- delete from `SALESMANAGER`.`PRODUCT_CATEGORY` where PRODUCT_ID = 4;
			-- delete from `SALESMANAGER`.`PRODUCT_DESCRIPTION` where PRODUCT_ID = 4;
            -- delete from SALESMANAGER.PRODUCT_PRICE where `PRODUCT_PRICE_ID` = 4;
            -- delete from SALESMANAGER.PRODUCT_PRICE_DESCRIPTION where `DESCRIPTION_ID` = 4;
           
		INSERT into SALESMANAGER.PRODUCT(
			`PRODUCT`.`PRODUCT_ID`,
            `DATE_CREATED`,
			`DATE_MODIFIED`,
			`PRODUCT`.`AVAILABLE`,
			`PRODUCT`.`DATE_AVAILABLE`,
			`PRODUCT`.`PREORDER`,
			`PRODUCT`.`PRODUCT_HEIGHT`,
			`PRODUCT`.`PRODUCT_FREE`,
			`PRODUCT`.`PRODUCT_LENGTH`,
			`PRODUCT`.`PRODUCT_SHIP`,
			`PRODUCT`.`PRODUCT_VIRTUAL`,
			`PRODUCT`.`PRODUCT_WEIGHT`,
			`PRODUCT`.`PRODUCT_WIDTH`,
			`PRODUCT`.`REF_SKU`,
			`PRODUCT`.`SKU`,
			`PRODUCT`.`SORT_ORDER`,
			`PRODUCT`.`MANUFACTURER_ID`,
			`PRODUCT`.`MERCHANT_ID`,
			`PRODUCT`.`PRODUCT_TYPE_ID`) values
		(@counter -- inc
        ,now()
		,now()
		,1
		,now()
		,0
		,1
		,0
		,1
		,1
		,0
		,1
		,1
		,@counter -- inc
		,@counter -- inc
		,@counter -- inc
		,1
		,1
		,50);
        

		
		-- SELECT * FROM SALESMANAGER.PRODUCT_AVAILABILITY;
			-- delete from SALESMANAGER.PRODUCT_AVAILABILITY where PRODUCT_ID = 4;
        
        
        insert into SALESMANAGER.PRODUCT_AVAILABILITY (
			`PRODUCT_AVAILABILITY`.`PRODUCT_AVAIL_ID`,
			`PRODUCT_AVAILABILITY`.`AVAILABLE`,
			`PRODUCT_AVAILABILITY`.`FREE_SHIPPING`,
			`PRODUCT_AVAILABILITY`.`QUANTITY`,
			`PRODUCT_AVAILABILITY`.`QUANTITY_ORD_MAX`,
			`PRODUCT_AVAILABILITY`.`QUANTITY_ORD_MIN`,
			`PRODUCT_AVAILABILITY`.`STATUS`,
			`PRODUCT_AVAILABILITY`.`REGION`,
			`PRODUCT_AVAILABILITY`.`MERCHANT_ID`,
			`PRODUCT_AVAILABILITY`.`PRODUCT_ID`
		) values (
			@counter
            ,1
            ,0
            ,1
            ,1
            ,1
            ,1
            ,'*'
            ,1
            ,@counter
		);
		

		
		-- SELECT * FROM SALESMANAGER.PRODUCT_PRICE;
			-- delete from SALESMANAGER.PRODUCT_PRICE where `PRODUCT_PRICE_ID` = 4;
        
        
        INSERT INTO `SALESMANAGER`.`PRODUCT_PRICE`
		(`PRODUCT_PRICE_ID`,
		`PRODUCT_PRICE_CODE`,
		`DEFAULT_PRICE`,
		`PRODUCT_PRICE_AMOUNT`,
		`PRODUCT_PRICE_TYPE`,
		`PRODUCT_AVAIL_ID`)
		VALUES
		(@counter,
		'base',
		1,
		1.00,
		'ONE_TIME',
		@counter);
		
		-- SELECT * FROM SALESMANAGER.PRODUCT_PRICE_DESCRIPTION;
			-- delete from SALESMANAGER.PRODUCT_PRICE_DESCRIPTION where `DESCRIPTION_ID` = 4;
        
        
        INSERT INTO `SALESMANAGER`.`PRODUCT_PRICE_DESCRIPTION`
		(`DESCRIPTION_ID`,
		`DATE_CREATED`,
		`DATE_MODIFIED`,
		`NAME`,
		`LANGUAGE_ID`,
		`PRODUCT_PRICE_ID`)
		VALUES
		(@counter,
		now(),
		now(),
		'DEFAULT',
		1,
		@counter);
        
        		-- SELECT * FROM SALESMANAGER.PRODUCT_DESCRIPTION;
			-- delete from `SALESMANAGER`.`PRODUCT_DESCRIPTION` where PRODUCT_ID = 4;
        
        
        INSERT INTO `SALESMANAGER`.`PRODUCT_DESCRIPTION`
		(`DESCRIPTION_ID`,
		`DATE_CREATED`,
		`DATE_MODIFIED`,
		`DESCRIPTION`,
		`NAME`,
		`TITLE`,
		`META_DESCRIPTION`,
		`META_KEYWORDS`,
		`PRODUCT_HIGHLIGHT`,
		`SEF_URL`,
		`LANGUAGE_ID`,
		`PRODUCT_ID`)
		VALUES
		(@counter,
		now(),
		now(),
		CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
        CONCAT('fasola', CAST(@counter as char(50))),
		1,
		@counter);

		-- SELECT * FROM SALESMANAGER.PRODUCT_CATEGORY;
			-- delete from `SALESMANAGER`.`PRODUCT_CATEGORY` where PRODUCT_ID = 4;
        
        INSERT INTO `SALESMANAGER`.`PRODUCT_CATEGORY`
		(`PRODUCT_ID`,
		`CATEGORY_ID`)
		VALUES (
			@counter
			,1
        );
        
        		
		-- SELECT * FROM SALESMANAGER.PRODUCT_IMAGE;
			-- delete from SALESMANAGER.PRODUCT_IMAGE where product_image_id = 4;

		insert into SALESMANAGER.PRODUCT_IMAGE (
		`PRODUCT_IMAGE`.`PRODUCT_IMAGE_ID`,
			`PRODUCT_IMAGE`.`DEFAULT_IMAGE`,
			`PRODUCT_IMAGE`.`IMAGE_CROP`,
			`PRODUCT_IMAGE`.`IMAGE_TYPE`,
			`PRODUCT_IMAGE`.`PRODUCT_IMAGE`,
			`PRODUCT_IMAGE`.`SORT_ORDER`,
			`PRODUCT_IMAGE`.`PRODUCT_ID`
		) values
		(@counter
		,1
		,0
		,0
		,'big_ziemniaki-ekologiczne.png'
		,0
		,@counter);

        
        SET @counter = @counter + 1;
	-- END WHILE;
 //
        
DELIMITER ;