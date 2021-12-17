CREATE PROCEDURE [dbo].[ProductosObtener]
      @Codigo int= NULL
AS BEGIN
  SET NOCOUNT ON

  SELECT 
     *     
    FROM dbo.Pedido P
    WHERE
    (@Id_Productos IS NULL OR Id_Productos=@Id_Productos)

END