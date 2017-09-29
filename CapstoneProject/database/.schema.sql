USE MealPlanning
GO

DROP TABLE ingred_by_recipe;
DROP TABLE ingredients;
DROP TABLE recipe_by_user;
DROP TABLE meal_plans;
DROP TABLE meal_plan_by_user;
DROP TABLE users;
DROP TABLE recipes;

USE master
GO

DROP DATABASE MealPlanning
GO

CREATE DATABASE MealPlanning
GO

USE MealPlanning
GO

CREATE TABLE users (
	user_id integer identity(1000,1) NOT NULL,
	email varchar(100) NOT NULL,
	password varchar(100) NOT NULL,
	CONSTRAINT pk_users_user_id PRIMARY KEY (user_id)
);

CREATE TABLE recipes (
	recipe_id integer identity(1,1) NOT NULL,
	is_master integer  NOT NULL, 
	recipe_name varchar(100) NOT NULL,
	description varchar(1000) NOT NULL,
	preparation varchar(5000) NOT NULL,
	CONSTRAINT pk_recipes_recipe_id PRIMARY KEY (recipe_id)
);

CREATE TABLE ingredients (
	ingredient_id integer identity(1,1) NOT NULL,
	ingredient_name varchar(100) NOT NULL,
	CONSTRAINT pk_ingredients_ingredient_id PRIMARY KEY (ingredient_id)
);

CREATE TABLE ingred_by_recipe (
	ingredient_id integer NOT NULL,
	recipe_id integer NOT NULL,
	amount varchar(100) NOT NULL,
	CONSTRAINT pk_ingred_by_recipe PRIMARY KEY (ingredient_id, recipe_id),
	CONSTRAINT fk_ingred_by_recipe_recipes FOREIGN KEY (recipe_id) REFERENCES recipes(recipe_id),
	CONSTRAINT fk_ingred_by_recipe_ingredients FOREIGN KEY (ingredient_id) REFERENCES ingredients(ingredient_id)	
);

CREATE TABLE recipe_by_user (
	user_id integer NOT NULL,
	recipe_id integer NOT NULL,
	CONSTRAINT pk_recipe_by_user PRIMARY KEY (user_id, recipe_id),
	CONSTRAINT fk_recipe_by_user_users FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT fk_recipe_by_user_recipes FOREIGN KEY (recipe_id) REFERENCES recipes(recipe_id)
);

CREATE TABLE meal_plan_by_user (
	meal_plan_id integer identity(1,1) NOT NULL,
	user_id integer NOT NULL,
	meal_plan_name varchar(100) NOT NULL,
	CONSTRAINT pk_meal_plan_by_user_meal_plan_id PRIMARY KEY (meal_plan_id),
	CONSTRAINT fk_meal_plan_by_user_user_id FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE meal_plans (
	meal_plan_index integer identity(1,1) NOT NULL,
	meal_plan_id integer NOT NULL,
	day integer NOT NULL,
	meal integer NOT NULL,
	recipe_id integer NOT NULL,
	CONSTRAINT pk_meal_plans_meal_plan_index PRIMARY KEY (meal_plan_index),
	CONSTRAINT fk_meal_plans_id FOREIGN KEY (meal_plan_id) REFERENCES meal_plan_by_user(meal_plan_id),
	CONSTRAINT fk_meal_plans_recipe_id FOREIGN KEY (recipe_id) REFERENCES recipes(recipe_id)
);

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

--HARVEST COBB SALAD RECIPE
INSERT INTO recipes VALUES (1, 'Harvest Cobb Salad', 'The perfect fall salad with the creamiest poppy seed salad dressing. So good, you’ll want to make this all year long!', 'To make the poppy seed dressing, whisk together mayonnaise, milk, sugar, apple cider vinegar and poppy seeds in a small bowl; set aside.
Heat a large skillet over medium high heat. Add bacon and cook until brown and crispy, about 6-8 minutes. Transfer to a paper towel-lined plate; set aside.
Place eggs in a large saucepan and cover with cold water by 1 inch. Bring to a boil and cook for 1 minute. Cover eggs with a tight-fitting lid and remove from heat; set aside for 8-10 minutes. Drain well and let cool before peeling and dicing.
To assemble the salad, place romaine lettuce in a large bowl; top with arranged rows of bacon, eggs, apple, pear, pecans, cranberries and goat cheese.
Serve immediately with poppy seed dressing.');
INSERT INTO ingredients VALUES ('bacon, diced');
INSERT INTO ingredients VALUES ('chopped romaine lettuce');
INSERT INTO ingredients VALUES ('apple, diced');
INSERT INTO ingredients VALUES ('pear, diced');
INSERT INTO ingredients VALUES ('Fisher Nuts Pecan Halves');
INSERT INTO ingredients VALUES ('dried cranberries');
INSERT INTO ingredients VALUES ('crumbled goat cheese');
INSERT INTO ingred_by_recipe VALUES (17, 4, '4 slices');
INSERT INTO ingred_by_recipe VALUES (2, 4, '2');
INSERT INTO ingred_by_recipe VALUES (18, 4, '6 cups');
INSERT INTO ingred_by_recipe VALUES (19, 4, '1');
INSERT INTO ingred_by_recipe VALUES (20, 4, '1');
INSERT INTO ingred_by_recipe VALUES (21, 4, '1/2 cup');
INSERT INTO ingred_by_recipe VALUES (22, 4, '1/3 cup');
INSERT INTO ingred_by_recipe VALUES (23, 4, '1/3 cup');

--MealPlan Data
INSERT INTO recipes VALUES (0,'Ramen','Cheap noodle dish', 'Bring 2 1/2 cups of water to a boil in a small saucepan. Add the noodles and cook for 2 minutes. Add the flavor packet, stir, and continue to cook for another 30 seconds.
Remove the pan from the heat and carefully add the egg. Do not stir; pull the noodles over the egg and let sit for one minute to poach.
Carefully transfer everything to a serving bowl, add the butter, cheese and sesame seeds and mix. Garnish with the scallions if desired.');
INSERT INTO ingred_by_recipe VALUES (1, 5, '1 pack');
INSERT INTO ingred_by_recipe VALUES (2, 5, '1');
INSERT INTO ingred_by_recipe VALUES (3, 5, '1/2 teaspoon');
INSERT INTO ingred_by_recipe VALUES (4, 5, '2 slices');
INSERT INTO ingred_by_recipe VALUES (5, 5, '1/4 teaspoon');
INSERT INTO ingred_by_recipe VALUES (6, 5, '1/2');
INSERT INTO recipe_by_user VALUES (1000, 5);
INSERT INTO meal_plan_by_user VALUES (1000, 'My First Meal Plan!');
INSERT INTO meal_plans VALUES (1, 1, 1, 5);
INSERT INTO meal_plans VALUES (1, 2, 2, 5);
INSERT INTO meal_plans VALUES (1, 3, 3, 5);
INSERT INTO meal_plans VALUES (1, 3, 3, 5);