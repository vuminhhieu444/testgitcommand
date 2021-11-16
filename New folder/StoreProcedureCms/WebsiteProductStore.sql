-------------- Insert -------------------------
create PROCEDURE [dbo].[website_product_create]
(@product_id uniqueidentifier , @product_name_l nvarchar(100) , @product_name_e nvarchar(100) , @product_description_l nvarchar(-1) , @product_description_e nvarchar(-1) , @product_link varchar(500) , @product_image varchar(500) , @sort_order int , @created_by_user_id uniqueidentifier ,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
        
        DECLARE @active_flag INT; 
		BEGIN TRY	
		
        SELECT  @active_flag=ISNULL(w.active_flag ,-1)
		FROM dbo.website_product w
		WHERE w.product_id = @product_id

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_product_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_product
                          SET 
                              		product_name_l = @product_name_l,
		product_name_e = @product_name_e,
		product_description_l = @product_description_l,
		product_description_e = @product_description_e,
		product_link = @product_link,
		product_image = @product_image,
		sort_order = @sort_order,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_product.product_id =@product_id;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_product
                        (
                          product_id , product_name_l , product_name_e , product_description_l , product_description_e , product_link , product_image , sort_order , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @product_id , @product_name_l , @product_name_e , @product_description_l , @product_description_e , @product_link , @product_image , @sort_order , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
                        );
                       
                END;
		    END; 
		END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Update -------------------------
CREATE PROCEDURE [dbo].[website_product_update]
(@product_id uniqueidentifier , @product_name_l nvarchar(100) , @product_name_e nvarchar(100) , @product_description_l nvarchar(-1) , @product_description_e nvarchar(-1) , @product_link varchar(500) , @product_image varchar(500) , @sort_order int , @lu_user_id uniqueidentifier ,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		
		BEGIN TRY	
				UPDATE website_product
		SET
		product_name_l = @product_name_l,
		product_name_e = @product_name_e,
		product_description_l = @product_description_l,
		product_description_e = @product_description_e,
		product_link = @product_link,
		product_image = @product_image,
		sort_order = @sort_order,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	product_id = @product_id;
			;
	END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Search -------------------------
 CREATE PROCEDURE [dbo].[website_product_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @product_name nvarchar(100),
@OUT_TOTAL_ROW  INT OUTPUT,
@OUT_ERR_CD  INT OUTPUT  ,        
@OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
        IF(@page_size <> 0)
            BEGIN
                IF(@lang = 'e')
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                               	w.product_name_e product_name, 
	w.product_id, 
	w.product_link, 
	w.product_image, 
	w.sort_order
                        INTO #Results1
                        FROM dbo.[website_product] AS w
                        WHERE w.active_flag = 1  AND (@product_name = '' OR w.product_name_e LIKE ('%' + @product_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)
                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results1
                       	WHERE ROWNUMBER BETWEEN ((@page_index - 1) * @page_size) + 1 AND(@page_index*@page_size)

                        DROP TABLE #Results1;
                END;
                    ELSE
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                              	w.product_name_l product_name, 
	w.product_id, 
	w.product_link, 
	w.product_image, 
	w.sort_order
                        INTO #Results2
                        FROM dbo.[website_product] AS w
                        WHERE w.active_flag = 1  AND (@product_name = '' OR w.product_name_l LIKE ('%' + @product_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)
                        
                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results2;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results2
                       	WHERE ROWNUMBER BETWEEN ((@page_index - 1) * @page_size) + 1 AND(@page_index*@page_size)

                        DROP TABLE #Results2;
                END;
        END;
            ELSE
            BEGIN
                IF(@lang = 'e')
                    BEGIN
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                               	w.product_name_e product_name, 
	w.product_id, 
	w.product_link, 
	w.product_image, 
	w.sort_order
                        INTO #Results3
                        FROM dbo.[website_product] AS w
                        WHERE w.active_flag = 1  AND (@product_name = '' OR w.product_name_e LIKE ('%' + @product_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)

                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results3;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results3;

                        DROP TABLE #Results3;
                END;
                    ELSE
                    BEGIN
                        
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY sort_order ASC)) AS RowNumber, 
                              	w.product_name_l product_name, 
	w.product_id, 
	w.product_link, 
	w.product_image, 
	w.sort_order
                        INTO #Results4
                        FROM dbo.[website_product] AS w
                        WHERE w.active_flag = 1  AND (@product_name = '' OR w.product_name_l LIKE ('%' + @product_name + '%'))
						OPTION (OPTIMIZE FOR UNKNOWN)

                        SELECT @OUT_TOTAL_ROW = COUNT(*)
                        FROM #Results4;

                        SELECT *, 
                               @OUT_TOTAL_ROW AS RecordCount
                        FROM #Results4;

                        DROP TABLE #Results4;
                END;
        END;
	END TRY
	BEGIN CATCH
		    
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- GetbyId -------------------------
CREATE PROCEDURE [dbo].[website_product_get_by_id]
(
 @product_id uniqueidentifier,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
        SELECT 
        	w.product_id,
	w.product_name_l,
	w.product_name_e,
	w.product_description_l,
	w.product_description_e,
	w.product_link,
	w.product_image,
	w.sort_order,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_product] AS w
        WHERE w.active_flag = 1 AND w.product_id = @product_id
		OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
-------------- Delete -------------------------
CREATE PROCEDURE [dbo].[website_product_delete_multi]
(@json_list_id NVARCHAR(MAX), 
 @updated_by   uniqueidentifier,
 @OUT_ERR_CD  INT OUTPUT  ,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET XACT_ABORT ON;
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
				-- update thong tin website_product
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_product T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.product_id') AS product_id
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.product_id=T2.product_id
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_product AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.product_id') AS product_id
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.product_id=tmp.product_id
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
