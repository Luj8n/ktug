use itertools::Itertools;
use std::{fs, time::Instant};

fn main() {
  let path = "../../input/U3_2.txt";
  let input = fs::read_to_string(path).expect(&format!("Couldn't read {}", path));

  struct Base {
    map: Vec<Vec<usize>>,
    min_price: usize,
    min_path: Vec<usize>,
  }

  let mut base = Base {
    map: input
      .lines()
      .skip(1)
      .map(|l| {
        l.split_whitespace()
          .map(|x| x.parse::<usize>().expect("Parsing error"))
          .collect_vec()
      })
      .collect_vec(),
    min_price: 10000000,
    min_path: vec![],
  };

  fn add_to_copy(v: &Vec<usize>, x: usize) -> Vec<usize> {
    let mut cp = v.clone();
    cp.push(x);
    cp
  }

  fn recur(
    current_price: usize,
    collected_nodes: &mut Vec<usize>,
    last_node: usize,
    base: &mut Base,
  ) {
    let n = base.map.len();

    if current_price >= base.min_price {
      return;
    }

    if collected_nodes.len() == n {
      let return_price = base.map[last_node][0];
      let current_price = current_price + return_price;
      collected_nodes.push(0);
      if current_price < base.min_price {
        base.min_price = current_price;
        base.min_path = collected_nodes.clone();
      }
      return;
    }

    for next_node in 0..n {
      if !collected_nodes.contains(&next_node) {
        let price = base.map[last_node][next_node];
        recur(
          current_price + price,
          &mut add_to_copy(collected_nodes, next_node),
          next_node,
          base,
        );
      }
    }
  }

  let start_time = Instant::now();

  recur(0, &mut vec![0], 0, &mut base);

  let diff = start_time.elapsed().as_millis();
  println!("Time taken: {:?}ms", diff);

  let ans = format!(
    "{} = {}",
    base.min_path.iter().map(|x| x + 1).join(", "),
    base.min_price
  );
  println!("{}", ans);
}
