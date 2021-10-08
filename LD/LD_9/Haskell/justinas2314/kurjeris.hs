import Data.List as L
import qualified Data.Set as S


main :: IO ()
main = do
  n <- readLn
  matrix <- readLines n
  let temp = findPath 0 matrix $ S.singleton 0
  putStrLn $ show $ reverse $ map (+ 1) (fst temp)
  putStrLn $ show $ snd temp


readLines :: Int -> IO [[Int]]
readLines 0 = return []
readLines n = do
  line <- getLine
  res <- readLines (n - 1)
  return $ (map read $ words line) : res


findPath :: Int -> [[Int]] -> S.Set Int -> ([Int], Int)
findPath i m s
  | S.size s == lm = ([i, 0], head (m !! i))
  | otherwise = L.minimumBy (\x y -> compare (snd x) (snd y)) [change i j (findPath j m (j `S.insert` s)) | j <- [0..lm - 1], not (j `S.member` s)]
  where
    lm = length m
    change i j x = (i : fst x, ((m !! i) !! j) + snd x)
