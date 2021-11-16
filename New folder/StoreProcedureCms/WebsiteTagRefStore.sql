-------------- Insert -------------------------
create PROCEDURE [dbo].[website_tag_ref_create]
(@tag_rcd varchar(50) , @sort_order int , @tag_name_l nvarchar(1000) , @tag_name_e nvarchar(1000) , @tag_description_l nvarchar(1500) , @tag_description_e nvarchar(1500) , @created_by_user_id uniqueidentifier ,
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
		FROM dbo.website_tag_ref w
		WHERE w.tag_rcd = @tag_rcd

        IF(@active_flag=1)
            BEGIN
                SELECT 'MESSAGE.website_tag_ref_exist';
			 END;
            ELSE
            BEGIN
                IF(@active_flag=0)
                    BEGIN
                        UPDATE dbo.website_tag_ref
                          SET 
                              		sort_order = @sort_order,
		tag_name_l = @tag_name_l,
		tag_name_e = @tag_name_e,
		tag_description_l = @tag_description_l,
		tag_description_e = @tag_description_e,
		created_by_user_id = @created_by_user_id,
lu_updated = GETDATE(),
  
                              active_flag = 1
                        WHERE website_tag_ref.tag_rcd =@tag_rcd;
                       
                END;
                    ELSE
                    BEGIN
                        INSERT INTO dbo.website_tag_ref
                        (
                          tag_rcd , sort_order , tag_name_l , tag_name_e , tag_description_l , tag_description_e , created_by_user_id , created_date_time , lu_updated , lu_user_id , active_flag 
                        )
                        VALUES
                        (
                          @tag_rcd , @sort_order , @tag_name_l , @tag_name_e , @tag_description_l , @tag_description_e , @created_by_user_id , GETDATE() , GETDATE() , @created_by_user_id , 1 
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
CREATE PROCEDURE [dbo].[website_tag_ref_update]
(@tag_rcd varchar(50) , @sort_order int , @tag_name_l nvarchar(1000) , @tag_name_e nvarchar(1000) , @tag_description_l nvarchar(1500) , @tag_description_e nvarchar(1500) , @lu_user_id uniqueidentifier ,
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
		IF(NOT EXISTS(SELECT 1 FROM website_tag_ref AS w WHERE w.tag_rcd = @tag_rcd AND w.active_flag = 1))
BEGIN
 SELECT 'MESSAGE.tag_rcd_not_exist';
END; ELSE 
 BEGIN 
		UPDATE website_tag_ref
		SET
		sort_order = @sort_order,
		tag_name_l = @tag_name_l,
		tag_name_e = @tag_name_e,
		tag_description_l = @tag_description_l,
		tag_description_e = @tag_description_e,
		lu_user_id = @lu_user_id,
lu_updated = GETDATE()
		WHERE 	tag_rcd = @tag_rcd;
			END;
	END TRY
	BEGIN CATCH 
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
      
	END CATCH
    END;
-------------- Search -------------------------
 CREATE PROCEDURE [dbo].[website_tag_ref_search] 
(
@page_index   INT, 
@page_size    INT, 
@lang         CHAR(1)
 , @tag_rcd varchar(50) , @tag_name nvarchar(1000),
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
                               	w.tag_name_e tag_name, 
	w.tag_description_e tag_description, 
	w.tag_rcd, 
	w.sort_order
                        INTO #Results1
                        FROM dbo.[website_tag_ref] AS w
                        WHERE w.active_flag = 1  AND (@tag_name = '' OR w.tag_name_e LIKE ('%' + @tag_name + '%')) AND  ( @tag_rcd = '' OR w.tag_rcd LIKE ('%' + @tag_rcd + '%'))
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
                              	w.tag_name_l tag_name, 
	w.tag_description_l tag_description, 
	w.tag_rcd, 
	w.sort_order
                        INTO #Results2
                        FROM dbo.[website_tag_ref] AS w
                        WHERE w.active_flag = 1  AND (@tag_name = '' OR w.tag_name_l LIKE ('%' + @tag_name + '%')) AND  ( @tag_rcd = '' OR w.tag_rcd LIKE ('%' + @tag_rcd + '%'))
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
                               	w.tag_name_e tag_name, 
	w.tag_description_e tag_description, 
	w.tag_rcd, 
	w.sort_order
                        INTO #Results3
                        FROM dbo.[website_tag_ref] AS w
                        WHERE w.active_flag = 1  AND (@tag_name = '' OR w.tag_name_e LIKE ('%' + @tag_name + '%')) AND  ( @tag_rcd = '' OR w.tag_rcd LIKE ('%' + @tag_rcd + '%'))
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
                              	w.tag_name_l tag_name, 
	w.tag_description_l tag_description, 
	w.tag_rcd, 
	w.sort_order
                        INTO #Results4
                        FROM dbo.[website_tag_ref] AS w
                        WHERE w.active_flag = 1  AND (@tag_name = '' OR w.tag_name_l LIKE ('%' + @tag_name + '%')) AND  ( @tag_rcd = '' OR w.tag_rcd LIKE ('%' + @tag_rcd + '%'))
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
CREATE PROCEDURE [dbo].[website_tag_ref_get_by_id]
(
 @tag_rcd varchar(50),
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
        	w.tag_rcd,
	w.sort_order,
	w.tag_name_l,
	w.tag_name_e,
	w.tag_description_l,
	w.tag_description_e,
	w.created_by_user_id,
	w.created_date_time,
	w.lu_updated,
	w.lu_user_id,
	w.active_flag
        FROM dbo.[website_tag_ref] AS w
        WHERE w.active_flag = 1 AND w.tag_rcd = @tag_rcd
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
CREATE PROCEDURE [dbo].[website_tag_ref_delete_multi]
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
				-- update thong tin website_tag_ref
				UPDATE T1
					SET 
						T1.active_flag = 0, 
						T1.lu_user_id = @updated_by, 
						T1.lu_updated = GETDATE()
				FROM dbo.website_tag_ref T1
				INNER JOIN (
					SELECT JSON_VALUE(p.value, '$.tag_rcd') AS tag_rcd
					FROM OPENJSON(@json_list_id) AS p
				) T2 on T1.tag_rcd=T2.tag_rcd
				
				--lay ra danh sach
				SELECT *
				FROM dbo.website_tag_ref AS T
				INNER JOIN (
								SELECT JSON_VALUE(p.value, '$.tag_rcd') AS tag_rcd
								FROM OPENJSON(@json_list_id) AS p
							) tmp on T.tag_rcd=tmp.tag_rcd
				OPTION (OPTIMIZE FOR UNKNOWN)
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
-------------- GetListDropdown -------------------------
CREATE PROCEDURE [dbo].[website_tag_ref_get_list_dropdown]
(
 @lang CHAR,
 @OUT_ERR_CD  INT OUTPUT,        
 @OUT_ERR_MSG NVARCHAR(MAX) OUTPUT 
)
AS
    BEGIN
		SET NOCOUNT ON;
		SET ARITHABORT ON
		SET @OUT_ERR_CD  = 0;
		SET @OUT_ERR_MSG = NULL;
		BEGIN TRY	
			IF(@lang = 'e')
				BEGIN
					SELECT
					  	w.tag_name_e label,
	w.tag_rcd value
,w.sort_order sort_order
					FROM dbo.[website_tag_ref] AS w
					WHERE w.active_flag = 1 
					ORDER BY sort_order ASC, "tag_name_e" ASC;
					
			END;
				ELSE
				BEGIN
					 SELECT
						w.tag_name_l label,
	w.tag_rcd value
,w.sort_order sort_order
					FROM dbo.[website_tag_ref] AS w
					WHERE w.active_flag = 1 
					ORDER BY sort_order ASC, "tag_name_l" ASC;
			END;
	END TRY
	BEGIN CATCH
		-- Set error code
		SET @OUT_ERR_CD  = @@ERROR;
		-- Set error message
		SET @OUT_ERR_MSG = ERROR_MESSAGE();
	END CATCH
    END;
