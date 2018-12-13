
--DELETE FROM PZ.ContentIngredient
DELETE FROM PZ.Locationingredient
--DELETE FROM PZ.Ingredient
DELETE FROM PZ.OrderContent
DELETE FROM PZ.[Order]
--DELETE FROM PZ.Content
DELETE FROM PZ.[User]
DELETE FROM PZ.[Location]

INSERT INTO PZ.Content(Name, Price)
VALUES ('Cheese Pizza', 10.99),
('Pepperoni Pizza', 13.99),
('Supreme Pizza', 15.99)

INSERT INTO PZ.Ingredient(Name)
VALUES ('Pepperoni'),
('Olives'),
('Sausage'),
('Bell Pepper')

INSERT INTO PZ.ContentIngredient(ContentId,IngredientId)
VALUES (2,1),
(3,2),
(3,3),
(3,4)

select * from PZ.[User]

select * from PZ.[Location]

select * from PZ.Locationingredient

select * from PZ.Ingredient

select * from PZ.Content

select * from PZ.[Order]

delete from PZ.[Order] where LocationId IS NULL

select *  from PZ.OrderContent