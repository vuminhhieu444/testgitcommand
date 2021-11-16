-------------- Insert -------------------------
create PROCEDURE [dbo].[website_slide_create]
(@slide_id uniqueidentifier , @slide_title_l nvarchar(250) , @slide_title_e nvarchar(250) , @slide_image nvarchar(250) , @slide_url varchar(200) , @slide_type int , @sort_order int , @is_show bit , @created_by_user_id uniqueidentifier ,
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
		FROM dbo.website_slide w
		WHERE w.slide_id = @slide_id

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_slide_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_slide
                          SET 
                              		slide_title_l = @slide_title_l,
		slide_title_e = @slide_title_e,
		slide_image = @slide_image,
		slide_url = @slide_url,
		slide_type = @slide_type,
		sort_order = @sort_order,
		is_show = @is_show,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_slide.slide_id =@slide_id;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_slide
                        (
                          slide_id , slide_title_l , slide_title_e , slide_image , slide_url , slide_type , sort_order , is_show , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @slide_id , @slide_title_l , @slide_title_e , @slide_image , @slide_url , @slide_type , @sort_order , @is_show , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
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
CREATE PROCEDURE [dbo].[website_slide_update]
(@slide_id uniqueidentifier , @slide_title_l nvarchar(250) , @slide_title_e nvarchar(250) , @slide_image nvarchar(250) , @slide_url varchar(200) , @slide_type int , @sort_order int , @is_show bit , @lu_user_id uniqueidentifier ,
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
				UPDATE website_slide
		SET
		slide_title_l = @slide_title_l,
		slide_title_e = @slide_title_e,
		slide_image = @slide_image,
		slide_url = @slide_url,
		slide_type = @slide_type,
		sort_order = @sort_order,
		is_show = @is_show,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	slide_id = @slide_id;
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
 CREATE PROCEDURE [dbo].[website_slide_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @slide_title nvarchar(250),
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
                               	w.slide_title_e slide_title, 
	w.slide_id, 
	w.slide_image, 
	w.slide_url, 
	w.slide_type, 
	w.sort_order, 
	w.is_show
                        INTO #Results1
                        FROM dbo.[website_slide] AS w
                        WHERE w.active_flag = 1  AND (@slide_title = '' OR w.slide_title_e LIKE ('%' + @slide_title + '%'))
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
                              	w.slide_title_l slide_title, 
	w.slide_id, 
	w.slide_image, 
	w.slide_url, 
	w.slide_type, 
	w.sort_order, 
	w.is_show
                        INTO #Results2
                        FROM dbo.[website_slide] AS w
                        WHERE w.active_flag = 1  AND (@slide_title = '' OR w.slide_title_l LIKE ('%' + @slide_title + '%'))
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
                               	w.slide_title_e slide_title, 
	w.slide_id, 
	w.slide_image, 
	w.slide_url, 
	w.slide_type, 
	w.sort_order, 
	w.is_show
                        INTO #Results3
                        FROM dbo.[website_slide] AS w
                        WHERE w.active_flag = 1  AND (@slide_title = '' OR w.slide_title_e LIKE ('%' + @slide_title + '%'))
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
                              	w.slide_title_l slide_title, 
	w.slide_id, 
	w.slide_image, 
	w.slide_url, 
	w.slide_type, 
	w.sort_order, 
	w.is_show
                        INTO #Results4
                        FROM dbo.[website_slide] AS w
                        WHERE w.active_flag = 1  AND (@slide_title = '' OR w.slide_title_l LIKE ('%' + @slide_title + '%'))
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
CREATE PROCEDURE [dbo].[website_slide_get_by_id]
(
 @slide_id uniqueidentifier,
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
        	w.slide_id,
	w.slide_title_l,
	w.slide_title_e,
	w.slide_image,
	w.slide_url,
	w.slide_type,
	w.sort_order,
	w.is_show,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_slide] AS w
        WHERE w.active_flag = 1 AND w.slide_id = @slide_id
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
CREATE PROCEDURE [dbo].[website_slide_delete_multi]
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
				-- update thong tin website_slide
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_slide T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.slide_id') AS slide_id
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.slide_id=T2.slide_id
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_slide AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.slide_id') AS slide_id
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.slide_id=tmp.slide_id
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
