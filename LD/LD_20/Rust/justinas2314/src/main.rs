use std::io;
use std::io::BufRead;
use std::collections::{HashMap, HashSet};

fn main() {
    let mut graph: HashMap<String, Vec<(String, usize)>> = HashMap::new();
    let mut visited = HashSet::new();
    let reader = io::stdin();
    let mut iterator = reader.lock().lines();
    let length: usize = iterator.next().unwrap().unwrap().parse().unwrap();
    let start = iterator.next().unwrap().unwrap();
    let mut temp1;
    let mut temp2;
    let mut temp3;
    let mut lines;
    for _ in 0..length {
        if let Ok(x) = iterator.next().unwrap() {
            lines = x.split(' ');
            temp1 = lines.next().unwrap().to_string();
            temp2 = lines.next().unwrap().to_string();
            temp3 = lines.next().unwrap().parse().unwrap();
            match graph.get_mut(&temp1) {
                Some(x) => {
                    x.push((temp2.clone(), temp3));
                },
                None => {
                    graph.insert(temp1.clone(), vec![(temp2.clone(), temp3)]);
                }
            }
            match graph.get_mut(&temp2) {
                Some(x) => {
                    x.push((temp1, temp3));
                },
                None => {
                    graph.insert(temp2, vec![(temp1, temp3)]);
                }
            }
        } else {
            break;
        }
    }
    let (path, result) = recurse(&graph, &mut visited, &start);
    println!("{}\n{}", result, path);
}


fn recurse<'a>(graph: &'a HashMap<String, Vec<(String, usize)>>, visited: &mut HashSet<&'a str>, start: &'a str) -> (String, usize) {
    let mut path;
    let mut temp;
    let mut temps;
    let mut maxstr = String::new();
    let mut maximum = 0;
    for i in graph.get(start).unwrap().iter() {
        if visited.contains(&*i.0) {
            continue;
        }
        visited.insert(start);
        match recurse(graph, visited, &i.0) {
            (x, y) => {
                temps = x;
                temp = y;
            }
        }
        temp += i.1;
        if temp > maximum {
            maximum = temp;
            maxstr = temps;
        }
        visited.remove(start);
    }
    if maxstr.len() == 0 {
        return (start.to_string(), maximum);
    }
    path = start.to_string();
    path.push_str(" -> ");
    path.push_str(&maxstr);
    (path, maximum)
}