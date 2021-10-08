import qualified Data.Set as S


main :: IO ()
main = do
  n <- readLn
  matrix <- readLines n
  let start = findStart matrix
  case start of
    Just n -> do
      putStrLn $ "Grafas yra \"skorpionas\""
      printStuff matrix n
    otherwise -> putStrLn "Grafas nera \"skorpionas\""



readLines :: Int -> IO [[Char]]
readLines 0 = return []
readLines n = do
  l <- getLine
  rest <- readLines (n - 1)
  return $ l : rest


findStart :: [[Char]] -> Maybe Int
findStart m = helper 0 m m
  where
    helper _ _ [] = Nothing
    helper n m (x:xs)
      | length (filter (== '+') x) /= 1 = helper (n + 1) m xs
      | isGeluonis m x (S.singleton n) = Just n
      | otherwise = helper (n + 1) m xs
    isGeluonis m xs s
      | length cases /= 1 = False
      | otherwise = isUodega m (m !! i) (i `S.insert` s)
      where
        cases = filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)
        i = fst $ head cases
    isUodega m xs s
      | length cases /= 1 = False
      | otherwise = isLiemuo m (m !! i) (i `S.insert` s)
      where
        cases = filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)
        i = fst $ head cases
    isLiemuo m xs s
      | length cases + S.size s == length m = True
      | otherwise = False
      where
        cases = filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)


printStuff :: [[Char]] -> Int -> IO ()
printStuff m n = do
  putStrLn $ "Geluonis: " ++ show n ++ " virsune"
  printGeluonis m (m !! n) (S.singleton n)
printGeluonis m xs s = do
  let i = fst $ head $ filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)
  putStrLn $ "Uodega:   " ++ show i ++ " virsune"
  printUodega m (m !! i) (i `S.insert` s)
printUodega m xs s = do
  let i = fst $ head $ filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)
  putStrLn $ "Liemuo:   " ++ show i ++ " virsune"
  printLiemuo m (m !! i) (i `S.insert` s)
printLiemuo m xs s = printKojos 1 $ filter (\(i, j) -> j == '+' && not (i `S.member` s)) (zip [0..] xs)
printKojos _ [] = do
  return ()
printKojos n ((i, j):xs) = do
  putStrLn $ show n ++ " koja:   " ++ show i ++ " virsune"
  printKojos (n + 1) xs
