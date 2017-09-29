USE MealPlanning
GO

--ADMIN USER
INSERT INTO users VALUES ('admin@website.com', 'admin123');

--RAMEN RECIPE
INSERT INTO recipes VALUES (1,'Ramen','Cheap noodle dish', 'Bring 2 1/2 cups of water to a boil in a small saucepan. Add the noodles and cook for 2 minutes. Add the flavor packet, stir, and continue to cook for another 30 seconds.
Remove the pan from the heat and carefully add the egg. Do not stir; pull the noodles over the egg and let sit for one minute to poach.
Carefully transfer everything to a serving bowl, add the butter, cheese and sesame seeds and mix. Garnish with the scallions if desired.');
INSERT INTO ingredients VALUES ('ramen noodles with flavor packet');
INSERT INTO ingredients VALUES ('large egg');
INSERT INTO ingredients VALUES ('butter');
INSERT INTO ingredients VALUES ('american cheese');
INSERT INTO ingredients VALUES ('toasted sesame seeds');
INSERT INTO ingredients VALUES ('scallion, optional');
INSERT INTO ingred_by_recipe VALUES (1, 1, '1 pack');
INSERT INTO ingred_by_recipe VALUES (2, 1, '1');
INSERT INTO ingred_by_recipe VALUES (3, 1, '1/2 teaspoon');
INSERT INTO ingred_by_recipe VALUES (4, 1, '2 slices');
INSERT INTO ingred_by_recipe VALUES (5, 1, '1/4 teaspoon');
INSERT INTO ingred_by_recipe VALUES (6, 1, '1/2');

--OMELETTE RECIPE
INSERT INTO recipes VALUES (1,'Omelette','Easy egg dish', 'Crack the eggs into a small bowl and whisk.
Add some salt and pepper, if you like.
Heat the oil or butter in a 9-inch non-stick frying pan and pour in the eggs.
In the first 30-seconds of cooking, use a spatula to create 6-10 small cuts through the omelette.
When the top is nearly set, sprinkle any fillings over half of the omelette and turn off the heat.
Use your spatula to flip one half of the omelette over the other and serve immediately.');
INSERT INTO ingredients VALUES ('salt');
INSERT INTO ingredients VALUES ('pepper');
INSERT INTO ingred_by_recipe VALUES (2, 2, '2');
INSERT INTO ingred_by_recipe VALUES (7, 2, 'to taste');
INSERT INTO ingred_by_recipe VALUES (8, 2, 'to taste');
INSERT INTO ingred_by_recipe VALUES (3, 2, '1 tablespoon');

--GRILLED CHEESE RECIPE
INSERT INTO recipes VALUES (1, 'Grilled Cheese', 'This grilled cheese sandwich has a garden twist', 'Spread 1/2 tablespoon of butter on one side of each piece of bread. Lie the slices of Cheddar on one of the slices of bread on the unbuttered side. Sprinkle the parsley, basil, oregano, rosemary, and dill on the other slice of bread on its unbuttered side. Sandwich the two slices of bread together with the buttered sides facing outwards.
Heat a skillet over medium heat. When skillet is hot, gently lie the sandwich in the skillet; cook on each side for 3 minutes until cheese has melted.');
INSERT INTO ingredients VALUES ('butter, softened');
INSERT INTO ingredients VALUES ('bread');
INSERT INTO ingredients VALUES ('sharp cheddar cheese');
INSERT INTO ingredients VALUES ('chopped parsley');
INSERT INTO ingredients VALUES ('chopped basil');
INSERT INTO ingredients VALUES ('oregano');
INSERT INTO ingredients VALUES ('chopped fresh rosemary');
INSERT INTO ingredients VALUES ('chopped fresh dill');
INSERT INTO ingred_by_recipe VALUES (9, 3, '1 tablespoon');
INSERT INTO ingred_by_recipe VALUES (10, 3, '2 slices');
INSERT INTO ingred_by_recipe VALUES (11, 3, '2 slices');
INSERT INTO ingred_by_recipe VALUES (12, 3, '1 tablespoon');
INSERT INTO ingred_by_recipe VALUES (13, 3, '1 teaspoon');
INSERT INTO ingred_by_recipe VALUES (14, 3, '1 teaspoon');
INSERT INTO ingred_by_recipe VALUES (15, 3, '1 teaspoon');
INSERT INTO ingred_by_recipe VALUES (16, 3, '1 teaspoon');

--MealPlan Data
INSERT INTO meal_plan_by_user VALUES (1000, 'Meal Plan One');
INSERT INTO meal_plans VALUES (1, 1, 1, 1);
INSERT INTO meal_plans VALUES (1, 1, 2, 1);
INSERT INTO meal_plans VALUES (1, 1, 3, 1);
INSERT INTO meal_plans VALUES (1, 2, 1, 1);
INSERT INTO meal_plans VALUES (1, 2, 2, 1);
INSERT INTO meal_plans VALUES (1, 2, 3, 1);
INSERT INTO meal_plans VALUES (1, 3, 1, 1);
INSERT INTO meal_plans VALUES (1, 3, 2, 1);
INSERT INTO meal_plans VALUES (1, 3, 3, 1);
INSERT INTO meal_plans VALUES (1, 4, 1, 1);
INSERT INTO meal_plans VALUES (1, 4, 2, 1);
INSERT INTO meal_plans VALUES (1, 4, 3, 1);
INSERT INTO meal_plans VALUES (1, 5, 1, 1);
INSERT INTO meal_plans VALUES (1, 5, 2, 1);
INSERT INTO meal_plans VALUES (1, 5, 3, 1);
INSERT INTO meal_plans VALUES (1, 6, 1, 1);
INSERT INTO meal_plans VALUES (1, 6, 2, 1);
INSERT INTO meal_plans VALUES (1, 6, 3, 1);
INSERT INTO meal_plans VALUES (1, 7, 1, 1);
INSERT INTO meal_plans VALUES (1, 7, 2, 1);
INSERT INTO meal_plans VALUES (1, 7, 3, 1);