using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class AddPostCommentHierarchyFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
                {
                    migrationBuilder.Sql
                    (@"
        				CREATE FUNCTION [dbo].[fn_PostCommentHierarchy] (@postId UNIQUEIDENTIFIER, @pageNumber INT, @pageSize INT)
        				RETURNS TABLE
        				AS
        				RETURN
        				(
        					WITH cte AS
        					(
        						SELECT CommentId, UserId, Content, PostId, ParentCommentId
        						FROM dbo.Comment
        						WHERE ParentCommentId IS NULL and PostId = @postId
        						ORDER BY PostId
        						OFFSET (@pageNumber - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
        
        						UNION ALL
        
        						SELECT child.CommentId, child.UserId, child.Content, child.PostId, child.ParentCommentId
        						FROM dbo.Comment child
        						INNER JOIN cte parent
        						ON parent.CommentId = child.ParentCommentId
        						WHERE parent.PostId = @postId
        					)
        
        					SELECT CommentId, UserId, Content, PostId, ParentCommentId FROM cte
        				);
        			");
                }
        
                protected override void Down(MigrationBuilder migrationBuilder)
                {
        	        migrationBuilder.Sql("DROP FUNCTION [dbo].[fn_PostCommentHierarchy]");
                }
    }
}
