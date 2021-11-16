-------------- Insert -------------------------
create PROCEDURE [dbo].[website_image_create]
(@image_id uniqueidentifier , @is_show bit , @image_name nvarchar(150) , @image_src varchar(200) , @sort_order int , @type int , @created_by_user_id uniqueidentifier ,
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
		FROM dbo.website_image w
		WHERE w.image_id = @image_id

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_image_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_image
                          SET 
                              		is_show = @is_show,
		image_name = @image_name,
		image_src = @image_src,
		sort_order = @sort_order,
		"type" = @type,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_image.image_id =@image_id;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_image
                        (
                          image_id , is_show , image_name , image_src , sort_order , "type" , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @image_id , @is_show , @image_name , @image_src , @sort_order , @type , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
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
CREATE PROCEDURE [dbo].[website_image_update]
(@image_id uniqueidentifier , @is_show bit , @image_name nvarchar(150) , @image_src varchar(200) , @sort_order int , @type int , @lu_user_id uniqueidentifier ,
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
				UPDATE website_image
		SET
		is_show = @is_show,
		image_name = @image_name,
		image_src = @image_src,
		sort_order = @sort_order,
		"type" = @type,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	image_id = @image_id;
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
 CREATE PROCEDURE [dbo].[website_image_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @image_name nvarchar(150),
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
                               	w.image_id, 
	w.is_show, 
	w.image_name, 
	w.image_src, 
	w.sort_order, 
	w.type
                        INTO #Results1
                        FROM dbo.[website_image] AS w
                        WHERE w.active_flag = 1  AND  ( @image_name = '' OR w.image_name LIKE ('%' + @image_name + '%'))
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
                              	w.image_id, 
	w.is_show, 
	w.image_name, 
	w.image_src, 
	w.sort_order, 
	w.type
                        INTO #Results2
                        FROM dbo.[website_image] AS w
                        WHERE w.active_flag = 1  AND  ( @image_name = '' OR w.image_name LIKE ('%' + @image_name + '%'))
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
                               	w.image_id, 
	w.is_show, 
	w.image_name, 
	w.image_src, 
	w.sort_order, 
	w.type
                        INTO #Results3
                        FROM dbo.[website_image] AS w
                        WHERE w.active_flag = 1  AND  ( @image_name = '' OR w.image_name LIKE ('%' + @image_name + '%'))
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
                              	w.image_id, 
	w.is_show, 
	w.image_name, 
	w.image_src, 
	w.sort_order, 
	w.type
                        INTO #Results4
                        FROM dbo.[website_image] AS w
                        WHERE w.active_flag = 1  AND  ( @image_name = '' OR w.image_name LIKE ('%' + @image_name + '%'))
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
CREATE PROCEDURE [dbo].[website_image_get_by_id]
(
 @image_id uniqueidentifier,
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
        	w.image_id,
	w.is_show,
	w.image_name,
	w.image_src,
	w.sort_order,
	w.type,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_image] AS w
        WHERE w.active_flag = 1 AND w.image_id = @image_id
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
CREATE PROCEDURE [dbo].[website_image_delete_multi]
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
				-- update thong tin website_image
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_image T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.image_id') AS image_id
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.image_id=T2.image_id
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_image AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.image_id') AS image_id
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.image_id=tmp.image_id
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
