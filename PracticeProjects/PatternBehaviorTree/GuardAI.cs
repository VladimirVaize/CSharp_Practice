using PatternBehaviorTree.Auxiliary;
using PatternBehaviorTree.Tree;
using System;
using System.Collections.Generic;

namespace PatternBehaviorTree
{
    public class GuardAI
    {
        public Vector2 Position { get; set; }
        private List<Vector2> _patrolPoints;
        private int _currentPatrolIndex;

        public bool CanSeePlayer { get; private set; }
        public bool IsAlerted { get; private set; }

        public PlayerController Player { get; set; }

        private int _waitTimer;
        private const int WAIT_DURATION = 3;

        private BTNode _rootNode;

        public GuardAI(Vector2 startPosition, List<Vector2> patrolPoints)
        {
            Position = startPosition;
            _patrolPoints = patrolPoints;
            _currentPatrolIndex = 0;
            CanSeePlayer = false;
            IsAlerted = false;
            _waitTimer = 0;

            BuildBehaviorTree();
        }

        public void Update()
        {
            UpdatePlayerVisibility();

            var result = _rootNode.Evaluate();

            Console.WriteLine($"Guard state: {result}");
        }

        private void BuildBehaviorTree()
        {
            var chaseAction = new ActionNode(ChasePlayer);
            var patrolAction = new ActionNode(Patrol);
            var waitAction = new ActionNode(Wait);

            var checkPlayerInSight = new Condition(CheckPlayerInSight);
            var isPlayerLost = new Condition(IsPlayerLost);

            var alertSequence = new Sequence(new List<BTNode>
            {
                checkPlayerInSight,
                chaseAction
            });

            var lostSequence = new Sequence(new List<BTNode>
            {
                isPlayerLost,
                waitAction
            });

            _rootNode = new Selector(new List<BTNode>
            {
                alertSequence,
                lostSequence,
                patrolAction
            });
        }

        private void UpdatePlayerVisibility()
        {
            if (Player != null)
            {
                double distance = Vector2.Distance(Position, Player.Position);
                bool wasSeeing = CanSeePlayer;
                CanSeePlayer = distance < 5;

                if (CanSeePlayer && !wasSeeing)
                {
                    IsAlerted = true;
                    Console.WriteLine($"!!! Охранник {Position} обнаружил игрока в {Player.Position} !!!");
                }

                if (!CanSeePlayer && IsAlerted)
                {
                    Console.WriteLine($"Охранник {Position} потерял игрока из виду");
                }
            }
        }

        private BTNode.NodeState ChasePlayer()
        {
            if (Player == null || !CanSeePlayer)
                return BTNode.NodeState.Failure;

            if (Position.X < Player.Position.X)
                Position = new Vector2(Position.X + 1, Position.Y);
            else if (Position.X > Player.Position.X)
                Position = new Vector2(Position.X - 1, Position.Y);

            if (Position.Y < Player.Position.Y)
                Position = new Vector2(Position.X, Position.Y + 1);
            else if (Position.Y > Player.Position.Y)
                Position = new Vector2(Position.X, Position.Y - 1);

            Console.WriteLine($"Преследую игрока: {Position}");

            _waitTimer = 0;

            return BTNode.NodeState.Success;
        }

        private BTNode.NodeState Patrol()
        {
            if (_patrolPoints.Count == 0)
                return BTNode.NodeState.Failure;

            Vector2 targetPoint = _patrolPoints[_currentPatrolIndex];

            if (Position.X < targetPoint.X)
                Position = new Vector2(Position.X + 1, Position.Y);
            else if (Position.X > targetPoint.X)
                Position = new Vector2(Position.X - 1, Position.Y);

            if (Position.Y < targetPoint.Y)
                Position = new Vector2(Position.X, Position.Y + 1);
            else if (Position.Y > targetPoint.Y)
                Position = new Vector2(Position.X, Position.Y - 1);

            if (Position.X == targetPoint.X && Position.Y == targetPoint.Y)
            {
                _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
                Console.WriteLine($"Достиг точки патруля, следующая: {_patrolPoints[_currentPatrolIndex]}");
            }

            Console.WriteLine($"Патрулирую: {Position} -> цель {targetPoint}");

            return BTNode.NodeState.Success;
        }

        private BTNode.NodeState Wait()
        {
            if (_waitTimer < WAIT_DURATION)
            {
                _waitTimer++;
                Console.WriteLine($"Ожидаю... ({_waitTimer}/{WAIT_DURATION})");
                return BTNode.NodeState.Running;
            }
            else
            {
                Console.WriteLine($"Закончил ожидание, возвращаюсь к патрулю");
                _waitTimer = 0;
                IsAlerted = false;
                return BTNode.NodeState.Success;
            }
        }

        private bool CheckPlayerInSight()
        {
            return CanSeePlayer;
        }

        private bool IsPlayerLost()
        {
            return IsAlerted && !CanSeePlayer;
        }
    }
}
