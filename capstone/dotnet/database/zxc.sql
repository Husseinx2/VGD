Select g.game_id, title, description, release_date from game g
join game_genre gg on gg.game_id = g.game_id
join game_developer gd on gd.game_id = g.game_id
join game_platform gpf on gpf.game_id = g.game_id
join company c on c.company_id = gd.developer_id
join game_publisher gpb on gpb.publisher_id = c.company_id
group by g.game_id, title, description, release_date

Select * from game
select * from game_genre
