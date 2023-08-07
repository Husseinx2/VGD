Select title, description, esrb_rating, release_date, genre_name from game g
join game_genre gg on gg.game_id = g.game_id
join game_developer gd on gd.game_id = g.game_id
join game_platform gpf on gpf.game_id = g.game_id
join company c on c.company_id = gd.developer_id
join game_publisher gpb on gpb.publisher_id = c.company_id
join game_developer ON gd.developer_id = c.company_id
join genre gen ON gen.genre_id = gg.genre_id
join platform p on p.platform_id = gpf.platform_id
group by title, description, esrb_rating, release_date, genre_name

Select * from game
select * from game_genre
select * from company
select * from users
select * from game_developer